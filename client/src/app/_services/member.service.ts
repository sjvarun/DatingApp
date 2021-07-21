import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Member } from '../_models/members';
import { environment } from '../../environments/environment';
//const httpOptions = {
//  headers: new HttpHeaders({ Authorization: 'Bearer ' + JSON.parse(localStorage.getItem("User"))?.token })
//}

@Injectable({
  providedIn: 'root'
})

export class MemberService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + "users")
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }
}
