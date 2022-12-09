import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from "rxjs";
import { TokenStorageService } from '../services/token-storage.service';


@Injectable({
    providedIn: 'root'
  })
  
export class AuthGuard implements CanActivate
{

    constructor(private tokenStorage:TokenStorageService, private router:Router) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

        if (this.tokenStorage.isTokenValid)
        {
            return true;
        }
        else
        {
            this.router.navigate(["/"]);
            return false;
        }

    }
    
}