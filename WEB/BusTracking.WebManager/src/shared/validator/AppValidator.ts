import { AbstractControl,ValidationErrors, AsyncValidatorFn } from '@angular/forms';

export class AppValidator{

    static cannotContainSpecialCharactor(control: AbstractControl) : ValidationErrors | null{
        var regex = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/
        if(regex.test(control.value)==true){
            return { containSpecial : true};
        }
        return null;
    }

    static cannotContainSpecDesc(control: AbstractControl) : ValidationErrors | null{
        var regex = /[!@#$%^&*()\[\]{}'"\\|<>\/]+/
        if(regex.test(control.value)==true){
            return { containSpecDesc : true};
        }
        return null;
    }
}