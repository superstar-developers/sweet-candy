import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../../types/data/users';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // public currentUser: Observable<User>;
  // private currentUserSubject: BehaviorSubject<User>;

  constructor() {
    // this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    // this.currentUser = this.currentUserSubject.asObservable();
  }

  // public get currentUserValue(): User {
  //   return this.currentUserSubject.value;
  // }

  login(username: string, password: string) {
    return true;
  }

}
