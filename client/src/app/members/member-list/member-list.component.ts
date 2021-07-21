import { Component, OnInit, Input } from '@angular/core';
import { MemberService } from '../../_services/member.service';
import { Member } from '../../_models/members';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  memberList: Member[];
  constructor(private member:MemberService) { }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.member.getMembers().subscribe(res => { this.memberList=res})
  }
}
