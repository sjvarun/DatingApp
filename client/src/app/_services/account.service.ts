import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import {map} from 'rxjs/operators'
import { User } from '../_models/User';
import { ReplaySubject } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((respose:User) => {
        const user = respose;
        localStorage.setItem('User', JSON.stringify(user));
        this.currentUserSource.next(user);
      })
      );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        localStorage.setItem('User', JSON.stringify(user));
        this.currentUserSource.next(user);
        return user;
      })
    )

  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logOut() {
    localStorage.removeItem('User');
    this.currentUserSource.next(null);
  }

}
