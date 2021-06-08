import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating App';
  constructor(private accountService: AccountService) { };

  ngOnInit(): void {
    
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('User'));
    this.accountService.setCurrentUser(user);
  }
}
