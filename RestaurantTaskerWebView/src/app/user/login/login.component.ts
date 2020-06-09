import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
//import {SpinnerOverlayService} from './../../shared/spinnerOverlay.service';
import { AuthService } from './../../shared/auth-sevice/auth.service';
import { from } from 'rxjs';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  }

  showSpinner: boolean = false;

  constructor(private service: UserService,
     private router: Router,
     private toastr: ToastrService,
     public authService: AuthService,
     //private spinnerOverlay: SpinnerOverlayService 
     ) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/home');
  }

  onSubmit(form: NgForm) {
    this.showSpinner = true;

    this.authService.login(form.value).subscribe(
      success => {
        if(success){
          this.router.navigateByUrl('/home');
        }
      },
      error => {
        if(error.status == 400){
          this.showSpinner = false;
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        }
      }
    );
    /*
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/home');
      },
      err => {
        if (err.status == 400){
          this.showSpinner = false;
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        }
        else
          console.log(err);
      }
    );*/
  }
}