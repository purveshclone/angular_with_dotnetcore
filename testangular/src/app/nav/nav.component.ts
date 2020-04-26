import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alretify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }
  login() {
    // tslint:disable-next-line: no-debugger
    debugger;
    this.authService.login(this.model).subscribe(next => {
      this.alretify.success('logged in successfully !!');
    }, error => {
      this.alretify.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    // const token = localStorage.getItem('token');
    // return !!token;
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alretify.message('Logged out successfully !! ');
    this.router.navigate(['/home']);
  }
}
