import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AlertService, ContactService } from '@app/_services';

@Component({ templateUrl: 'contact.component.html' })
export class ContactComponent implements OnInit {
    form: FormGroup;
    id: number;
    contact = null;

    constructor(
        private route: ActivatedRoute,
        private contactService: ContactService,
        private router: Router,
        private alertService: AlertService
    ) {}

    ngOnInit() {
        this.id = this.route.snapshot.params['id'];
        this.contactService.getById(this.id)
        .pipe(first())
        .subscribe(contact => this.contact = contact);
    }
    deleteContact(id: number) {
         this.contactService.Delete(id)  .subscribe(
            data => {                 
                this.alertService.success('Contato deletado com sucesso.', { keepAfterRouteChange: true });
                this.router.navigate(['..', { relativeTo: this.route }]);
            },
            error => {
                this.alertService.error(JSON.stringify(error).replace(/['"]+/g, ''));
            });
    }
}