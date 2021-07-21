import { Component, OnInit } from '@angular/core';
import { MemberService } from '../../_services/member.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/members';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  constructor(private memberService:MemberService,private router:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMemberDetails();

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        thumbnailsColumns: 4,
        imagePercent:100,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview:false
      }]
  }

  getImages(): NgxGalleryImage[] {
    const images = [];
    for (const item of this.member.photos) {
      images.push({
        small: item.url,
        medium: item.url,
        big:item.url
      })
    }
    return images;
  }

  loadMemberDetails() {
    this.memberService.getMember(this.router.snapshot.paramMap.get('username')).subscribe(data => {
      this.member = data;
      this.galleryImages = this.getImages();
    }); 
  }
}
