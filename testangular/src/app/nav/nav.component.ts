import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alretify: AlertifyService) { }

  ngOnInit() {
  }
  login() {
    // tslint:disable-next-line: no-debugger
    debugger;
    this.authService.login(this.model).subscribe(next => {
      this.alretify.success('logged in successfully !!');
    }, error => {
      this.alretify.error(error);
    });
  }

  loggedIn() {
    // const token = localStorage.getItem('token');
    // return !!token;
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alretify.message('Logged out successfully !! ')
  }
}
