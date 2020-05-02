import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'bearer ' + localStorage.getItem('token')
//   })
// };

@Injectable({
  providedIn: 'root'
})

export class UserService {
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    debugger;
    return this.http.get<User[]>(this.baseUrl + 'user/getusers');
  }
  getUser(id): Observable<User> {
    debugger;
    return this.http.get<User>(this.baseUrl + 'user/getuser?id=' + id);
  }
  updateUser(user: User) {
    return this.http.put(this.baseUrl + 'user/updateuser', user);
  }

  setMainPhoto(userId: number, id: number) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/setmain', {});
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id);
  }
}
