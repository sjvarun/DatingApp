import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { error } from '@angular/compiler/src/util';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(public accountService: AccountService, private router: Router, private toastrService: ToastrService) { }

  model: any = {}

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      console.log(response)
      this.router.navigateByUrl('/members');
    }, error => {
        console.log(error);
        this.toastrService.error(error.error);
    })
  }

  logOut() {
    this.accountService.logOut();
    this.router.navigateByUrl('');
  }
}
