using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TalkToApiStudyTest.V1.Models
{
    public class Message
    {


        [Key]
        public int Id { get; set; }



        [ForeignKey("FromId")]

        public ApplicationUser From { get; set; }

        [Required]
        public string FromId { get; set; }

        [ForeignKey("ToId")]
        public ApplicationUser To { get; set; }

        [Required]
        public string ToId { get; set; }


        [Required]
        public string Text { get; set; }

        public DateTime Created { get; set; }


        public DateTime? Updated { get; set; }


    }
}
