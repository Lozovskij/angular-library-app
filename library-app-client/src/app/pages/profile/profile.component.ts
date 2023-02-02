import { ChangeDetectorRef, Component } from '@angular/core';
import { BooksService } from 'src/app/shared/services/books.service';
import { BookInstance, BookInstanceStatus } from '../../shared/models/books';
import { PatronProfile, ProfileService } from './profile.service';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
    profile: PatronProfile | undefined;
    patronHolds: BookInstance[] | undefined;
    patronCheckouts: BookInstance[] | undefined;

    constructor(
        private profileService: ProfileService,
        private booksService: BooksService,
    ) {

        profileService.getPatronProfile().subscribe(p => {
            this.profile = p;
            this.patronHolds = this.profile.books.filter(b => b.status === BookInstanceStatus.OnHold);
            this.patronCheckouts = this.profile.books.filter(b =>
                b.status === BookInstanceStatus.CheckedOut ||
                b.status === BookInstanceStatus.Overdue);
        });
    }

    patronFullName(): string {
        return this.profile?.patron.firstName + " " + this.profile?.patron.lastName;
    }
    
    isOverdue(bookInstance: BookInstance) {
        return bookInstance.status === BookInstanceStatus.Overdue;
    }

    cancelHold(bookId: number) {
        this.booksService.cancelBookHold(bookId).subscribe({
            next: () => {
                this.patronHolds = this.patronHolds?.filter(b => b.bookId !== bookId);
            },
            error: (error) => console.log(error)
        });
    }
}
