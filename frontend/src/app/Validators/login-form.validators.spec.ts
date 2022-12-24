import { LoginFormValidators } from './login-form.validators';
import { FormControl, Validators } from '@angular/forms';


describe('login-form Validator',() =>
{
     
    var form:FormControl;


    it('LoginForm is invalid', () =>
    {   
        form = new FormControl("   ",[Validators.required,LoginFormValidators.notOnlyWhiteSpace]);
        expect(form.invalid).toBe(true);
    })

    it('LoginForm Email field is invalid', () =>
    {   
        form = new FormControl("lewisgmail.com",[Validators.required,LoginFormValidators.notOnlyWhiteSpace, Validators.email,Validators.minLength(10)]);
        expect(form.invalid).toBe(true);
    });

    it('login Email field is valid', () =>
    {
        form = new FormControl("lewis@gmail.com",[Validators.required,LoginFormValidators.notOnlyWhiteSpace, Validators.email,Validators.minLength(10)]);
        expect(form.valid).toBe(true);
    });

})