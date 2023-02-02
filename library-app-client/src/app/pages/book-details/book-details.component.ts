import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BooksService } from '../../shared/services/books.service';
import { Book, BookInstanceStatus } from '../../shared/models/books';

@Component({
    selector: 'app-book-details',
    templateUrl: './book-details.component.html',
    styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent {
    book: Book | null = null;
    status: BookInstanceStatus | undefined;
    showStatus: boolean = false;
    canHold: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private booksService: BooksService,
    ) { }

    ngOnInit(): void {
        const routeParams = this.route.snapshot.paramMap;
        const bookIdFromRoute = Number(routeParams.get('bookId'));
        this.booksService.getBook(bookIdFromRoute).subscribe(book => this.book = book);
        this.booksService.getBookInstanceStatus(bookIdFromRoute)
            .subscribe(status => {
                if (status !== null) {
                    this.status = status;
                    this.showStatus = true;
                    this.canHold = this.status?.toString() === BookInstanceStatus.Available.toString();
                }
            });
    }

    hold(): void {
        if (this.book === null) { return; }
        this.booksService.holdBook(this.book.id).subscribe(
            () => {
                this.status = BookInstanceStatus.OnHold;
                this.canHold = false;
            },
            (error) => console.log(error)
        );
    }
}
