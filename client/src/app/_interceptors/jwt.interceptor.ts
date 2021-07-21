import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/User';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
   currentUser: User; 
  constructor(private acountSrevice : AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.acountSrevice.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
    if (this.currentUser) {
      request = request.clone({
        setHeaders: {Authorization:`Bearer ${this.currentUser.token}`}
      })
    }
    return next.handle(request);
  }
}
