import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { ContactService } from '@app/_services';


@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    contacts = null;

    constructor(
        private contactService: ContactService,
        ) {}

    ngOnInit() {
        this.contactService.GetByUserId(localStorage.getItem('userLogged'))
            .pipe(first())
            .subscribe(contacts => this.contacts = contacts);
    }

    
    deleteContact(id: string) {
        const deleted = this.contacts.find(x => x.id === id);
        deleted.isDeleting = true;
        this.contactService.Delete(id)
            .pipe(first())
            .subscribe(() => {
                this.contacts = this.contacts.filter(x => x.id !== id) 
            });
    }
}