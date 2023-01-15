using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TalkToApiStudyTest.Helpers.Contants;
using TalkToApiStudyTest.Hub;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Models.dto;
using TalkToApiStudyTest.V1.Services.Contracts;

#pragma warning disable
namespace TalkToApiStudyTest.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Produces(CustomMediaType.Hateoas,CustomMediaType.returnXML, CustomMediaType.returnJSON)]
    [EnableCors]
    [Authorize]
    public class MessageController: ControllerBase
    {
        private IMessageService _messageRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<BroadcastHub, IClientHub> _hubContext;

        public MessageController(IMessageService? messageRepository, IMapper mapper, IHubContext<BroadcastHub, IClientHub>? hubContext)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

         [HttpGet("edit")]
         [MapToApiVersion("1.0")]
         [AllowAnonymous]
         public ActionResult<string> get()
        {
            return  Ok("ola lewis");
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{userOne}/{userTwo}", Name = "GetMessages")]
        public async Task<ActionResult<List<MessageDTO>>> GetMessages(string userOne, string userTwo,
            [FromHeader(Name ="Accept")] string? mediaType)

        {
            if (userOne == userTwo)
            {
                return UnprocessableEntity();
            }

            List<Message> messagesList = new List<Message>();

            try { 
                 messagesList = await _messageRepository.GetMessages(userOne, userTwo);
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }

            if (mediaType == CustomMediaType.Hateoas)
            {
                var newMessage = new List<Message>();
                newMessage = await _messageRepository.GetMessages(userOne, userTwo);
                List<MessageDTO> messagesDTO = _mapper.Map<List<Message>, List<MessageDTO>>(newMessage);
                _hubContext.Clients.All.brodcastConnectionId(userOne);
                ListDTO<MessageDTO> result = new ListDTO<MessageDTO>() { Result = messagesDTO };
                result.links.Add(new LinkDTO("_self", Url.Link("GetMessages", new { userOne = userOne, userTwo = userTwo }), "GET"));

                return Ok(result);

            }
             else
            {
                return Ok(messagesList);
            }
        }


        [MapToApiVersion("1.0")]
        [HttpPost(Name ="RegisterMessage")]
        public async Task<ActionResult> Register([FromBody] MessageConnectionId messageConnectionId,
            [FromHeader(Name ="Accept")] string? mediaType)
        {
            if(ModelState.IsValid)
            {
                try
                {
                     var message = _mapper.Map<MessageConnectionId,Message >(messageConnectionId);
                    _hubContext.Clients.AllExcept(messageConnectionId.ToConnectionId).brodcastNotification(messageConnectionId);
                    _messageRepository.Register(message);

                    if (mediaType == CustomMediaType.Hateoas)
                    {
                        MessageDTO messageDTO = _mapper.Map<Message, MessageDTO>(message);

                        messageDTO.links.Add(new LinkDTO("_self", Url.Link("RegisterMessage", new { }), "POST"));
                        messageDTO.links.Add(new LinkDTO("_ParcialUpdate", Url.Link("ParcialUpdate", new { id = messageConnectionId.Id }), "PUT"));

                        return Ok(messageDTO);
                    }
                    else
                    {
                        return Ok(message);
                    }
                }
                catch(Exception e)
                {
                    return UnprocessableEntity(e);
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpPatch("{id}",Name = "ParcialUpdate")]
         public async Task<ActionResult<MessageDTO>> PartialUpdate(int id, [FromBody] JsonPatchDocument<Message> jsonPatch,
            [FromHeader(Name ="Accept")] string mediaType)
            {

            if (jsonPatch.Operations.Count  < 1)
            {
                return BadRequest();
            }

            var message = await _messageRepository.Get(id);
            jsonPatch.ApplyTo(message);

            message.Updated = DateTime.UtcNow;
            _messageRepository.Update(message);

            if (mediaType == CustomMediaType.Hateoas)
            {
                MessageDTO messageDTO = _mapper.Map<Message, MessageDTO>(message);
                messageDTO.links.Add(new LinkDTO("_self", Url.Link("ParcialUpdate", new { id = message.Id }), "PUT"));

                return Ok(messageDTO);
            }

            else
            {
                return Ok(message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<Message>> Get(int id)
        {
            var message =await _messageRepository.Get(id);

            return Ok(message);
        }
    }
}
