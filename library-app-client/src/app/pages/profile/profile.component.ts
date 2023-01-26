import { Component } from '@angular/core';
import { BookInstance, BookInstanceStatus } from '../../shared/models/books';
import { PatronProfile, ProfileService } from './profile.service';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
    profile: PatronProfile | null = null;

    patronFullName(): string {
        if (this.profile === null) {return "";}
        return this.profile.patron.firstName + " " + this.profile.patron.lastName;
    }

    patronHolds(): BookInstance[] {
        if (this.profile === null) {return [];}
        return this.profile.books.filter(b => b.status === BookInstanceStatus.OnHold);
    }

    patronCheckouts(): BookInstance[] {
        if (this.profile === null) {return [];}
        return this.profile.books.filter(b =>
            b.status === BookInstanceStatus.CheckedOut ||
            b.status === BookInstanceStatus.Overdue);
    }

    isOverdue(bookInstance: BookInstance) {
        return bookInstance.status === BookInstanceStatus.Overdue;
    }

    constructor(profileService: ProfileService) {
        profileService.getPatronProfile().subscribe(p => {
            console.log(p);
            this.profile = p;
            console.log(this.patronHolds());
            console.log(this.patronCheckouts());

        });
    }
}
