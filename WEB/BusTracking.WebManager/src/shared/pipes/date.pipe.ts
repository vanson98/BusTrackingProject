import {Pipe,PipeTransform, Injector} from '@angular/core'

@Pipe({
    name : 'mydate'
})
export class AppDatePipe implements PipeTransform{
    
    constructor() { 
    }
    
    transform(value: any, ...args: any[]) {
        if(!value){
            return null;
        }
        let day = value._a[2];
        let month = value._a[1]+ 1;
        let year = value._a[0];
        return day+'/'+month+'/'+year
    }

}