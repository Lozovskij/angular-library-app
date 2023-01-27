import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs';
import { BooksService } from '../books-catalog/books.service';
import { Book, BookInstance, BookInstanceStatus } from '../../shared/models/books';

@Component({
    selector: 'app-book-details',
    templateUrl: './book-details.component.html',
    styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent {
    book: Book | null = null;

    getStatus(): BookInstanceStatus {
        return BookInstanceStatus.OnHold;
    }

    constructor(
        private route: ActivatedRoute,
        private booksService: BooksService,
    ) { }

    ngOnInit(): void {
        const routeParams = this.route.snapshot.paramMap;
        const bookIdFromRoute = Number(routeParams.get('bookId'));
        
        this.booksService.getBook(bookIdFromRoute).subscribe(book => this.book = book)
    }
}
