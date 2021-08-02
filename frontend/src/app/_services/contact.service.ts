import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Contact } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class ContactService {
    private contactSubject: BehaviorSubject<Contact>;
    public contact: Observable<Contact>;


    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.contactSubject = new BehaviorSubject<Contact>(JSON.parse(localStorage.getItem('contact')));
        this.contact = this.contactSubject.asObservable();
      }
    
    public get contactValue(): Contact {
        return this.contactSubject.value;
    }

    GetByUserId(id: string) {
        return this.http.get<Contact[]>(`${environment.apiUrl}/contacts/users/${id}`);
    }

    getById(id: number) {
        return this.http.get<Contact>(`${environment.apiUrl}/contacts/${id}`);
    }
    register(contact: Contact) {
        return this.http.post(`${environment.apiUrl}/contacts/register`, contact);
    }
    Put(id, params) {
        return this.http.put(`${environment.apiUrl}/contacts/update/${id}`, params)
    }

    Delete(id: number) {
        return this.http.delete(`${environment.apiUrl}/contacts/${id}`)
            .pipe(map(x => {
                return x;
            }));
    }
}