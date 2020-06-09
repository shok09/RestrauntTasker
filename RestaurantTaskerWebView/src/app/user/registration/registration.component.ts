import { UserService } from './../../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  showSpinner: boolean = false;

  constructor(public service: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.formModel.reset();
  }

  onSubmit() {
    this.showSpinner = true;
    this.service.register().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.service.formModel.reset();
          this.showSpinner = false;
          this.toastr.success('New user created!', 'Registration successful.');
        } else {
          res.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.showSpinner = false;
                this.toastr.error('Username is already taken','Registration failed.');
                break;

              default:
                this.showSpinner = false;
              this.toastr.error(element.description,'Registration failed.');
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
  }

}