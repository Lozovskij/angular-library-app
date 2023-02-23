import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { switchMap, tap } from 'rxjs';
import { BooksService } from 'src/app/shared/services/books.service';
import { BookInstance, BookInstanceStatus } from '../../shared/models/books';
import { PatronApi, PatronBookApi, ProfileService } from './profile.service';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProfileComponent {

    isLoading: boolean = true;
    patron!: PatronApi;
    patronBooks!: PatronBookApi[];
    patronHolds!: PatronBookApi[];
    patronCheckouts!: PatronBookApi[];


    constructor(
        profileService: ProfileService,
        private booksService: BooksService,
        cdr: ChangeDetectorRef,
    ) {
        profileService.getPatron().pipe(
            tap(p => this.patron = p),
            switchMap(_ => profileService.getPatronBooks()),
            tap(b => this.patronBooks = b.bookInstances)
        ).subscribe(_ => {
            this.patronHolds = this.patronBooks.filter(b => b.status === BookInstanceStatus.OnHold);
            this.patronCheckouts = this.patronBooks.filter(b =>
                b.status === BookInstanceStatus.CheckedOut ||
                b.status === BookInstanceStatus.Overdue);

            this.isLoading = false;
            cdr.markForCheck();
        });
    }

    patronFullName(): string {
        console.log('hello');
        return this.patron.firstName + " " + this.patron.lastName;
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
