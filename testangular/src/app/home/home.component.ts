import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  // values: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    // this.getvalues();
  }

  registerToggle() {
    this.registerMode = true;
  }
  // getvalues() {
  //   this.http.get('http://localhost:5000/api/values/GetValues').subscribe(response => {
  //     this.values = response;
  //     console.log(this.values);
  //   }, error => {
  //     console.log(error);
  //   });
  // }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
}
