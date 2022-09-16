import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { Login } from 'src/app/models/login';
import { UserService } from 'src/app/services/user.service';
import { LoginFormValidators } from 'src/app/Validators/login-form.validators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup;
  login:Login;
  errorActived:boolean = false;
  errorMessage:string = "";


  constructor( private formBuilder:FormBuilder, private userService: UserService , private router:Router) { }

  ngOnInit(): void {

    this.loginForm = this.formBuilder.group(
      {
        login: this.formBuilder.group({
          email: new FormControl("",[Validators.required,Validators.minLength(7),Validators.maxLength(40), Validators.email, LoginFormValidators.notOnlyWhiteSpace]),
          password: new FormControl("",[Validators.required,Validators.minLength(5),Validators.maxLength(30), LoginFormValidators.notOnlyWhiteSpace]),
        })
      });
  }


 get email()
  {
    return this.loginForm.get("login.email");
  }
 get password()
  {
    return this.loginForm.get("login.password");
  }

  onSubmit()
  {
    this.errorActived = false;
    this.errorMessage = "";

    if (this.loginForm.invalid)
    {
      this.loginForm.markAllAsTouched();
    }
    else
    {
      const email = this.loginForm.get("login")?.get("email")?.value;
      const password = this.loginForm.get("login")?.get("password")?.value;
       this.login = new Login(email,password);
  
       this.userService.loginUser(this.login).subscribe(
         
         data =>  {
            console.log(JSON.stringify(data))
            this.router.navigateByUrl("/message");
         },
         error =>  
         {
           console.log(error);
           this.errorsHandle(error);
         }
       )
       
    }

   
    
    
  }
  errorsHandle(error:any)
  {
    if (error.status == 401)
    {
      this.errorActived = true;
      this.errorMessage = "Your Email or Password is Incorrect!";
    }
    if (error.status == 0)
    {
      this.errorActived = true;
      this.errorMessage = "Please try to Log In again Later";
    }

    if (error.status == 500)
    {
      this.errorActived = true;
      this.errorMessage = "Please try to Log In again Later";
    }
    
  }

 

}
