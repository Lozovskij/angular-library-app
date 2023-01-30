import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs';
import { BooksService } from '../../shared/services/books.service';
import { Book, BookInstance, BookInstanceStatus } from '../../shared/models/books';

@Component({
    selector: 'app-book-details',
    templateUrl: './book-details.component.html',
    styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent {
    book: Book | null = null;
    status: BookInstanceStatus | undefined;
    showNoBookInstancesMessage: boolean = false;

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
                if (status === null)
                {
                    this.showNoBookInstancesMessage = true;
                }
                else
                {
                    this.status = status
                }
            });
    }

    hold(): void {
        if (this.book === null) { return; }
        this.booksService.holdBook(this.book.id).subscribe();
    }
}
