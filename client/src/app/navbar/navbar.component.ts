import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { error } from '@angular/compiler/src/util';
import { Observable } from 'rxjs';
import { User } from '../_models/User';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(public accountService: AccountService) { }

  model: any = {}

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      console.log(response)
    }, error => { console.log(error) })
  }

  logOut() {
    this.accountService.logOut();
  }
}
