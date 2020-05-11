import { Component, OnInit } from '@angular/core';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { LoggedUserService } from 'src/app/modules/shared/services/logged-user.service';
import { JamUserModel } from 'src/app/modules/shared/jam-user-info';
import { UserProfileService } from '../../services/user-profile.service';
import * as M from "materialize-css/dist/js/materialize";
import { Router } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  animations: [
    trigger('flyInOut', [
      transition('void => *',[
        style({transform: 'translateX(-100%)'}),
        animate('0.2s')
      ])
    ])
  ]
})
export class ProfileComponent implements OnInit {

  public isInputValid: boolean = true;
  public inputUserName: string;
  public currentUser :JamUserModel;
  public name :string;
  public loadedImage: string;
  showCropper: boolean = false;


  imageChangedEvent: any = '';
  croppedImage: any = '';
  text: string;
  constructor(private loggedUserService: LoggedUserService,
    private userProfileService : UserProfileService,
    private router: Router
    ) {
    this.currentUser=this.loggedUserService.getCurrentLoggedUser();
    this.croppedImage = this.currentUser.photoBase64;
    this.loadedImage = this.croppedImage;
    if(this.croppedImage)
      this.showCropper = true;
    }

  ngOnInit() {
    
  }
  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
 

    const files = event.target.files;
    const file = files[0];
    if (files && file) { 
      this.showCropper=true;     
    }
    
  }
  imageCropped(event: ImageCroppedEvent) {
      this.croppedImage = event.base64;
  }
  imageLoaded() {
      // show cropper
      console.log('loaded');
      
  }
  cropperReady() {
    console.log('ready');
      // cropper ready
  }
  loadImageFailed() {
      // show message
  }
  onUpdateClick()
  {  
    if(!this.currentUser.userName) {
      this.isInputValid=false;
      return;
    }
    this.currentUser.photoBase64 = this.croppedImage;
    this.userProfileService.updateUserInfo(this.currentUser).then(result => {
      this.loggedUserService.updateUserData();
      M.toast({html: 'Dane zostały pomyślnie zaktualizowane',displayLength: 1500,classes: 'rounded'})  ;
      this.router.navigate(["/"]);
    },
     error => {
      console.log(error);
  });
  }

}
