import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { TokenStorageService } from '../services/token-storage.service';



export class TokenInterceptor implements HttpInterceptor
{

    constructor(private tokenStorage:TokenStorageService){}

     private login:string  = "http://localhost:4200/login";

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        
        // if (this.tokenStorage.getToken() && req.url == this.login )
        // {
        //     const newRequest = req.clone({
        //         headers: req.headers.append("Authorization", "Bearer " + this.tokenStorage.getToken())

        //     })

        //     return next.handle(newRequest);
        // }

        
        return next.handle(req);
    }

}