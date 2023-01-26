import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap } from 'rxjs';
import { BooksService } from '../books-catalog/books.service';
import { Book, BookInstance } from '../../shared/models/books';

@Component({
    selector: 'app-book-details',
    templateUrl: './book-details.component.html',
    styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent {
    book: Book | null = null;
    bookInstanses: BookInstance[] | null = null;

    constructor(
        private route: ActivatedRoute,
        private booksService: BooksService,
    ) { }

    ngOnInit(): void {
        const routeParams = this.route.snapshot.paramMap;
        const bookIdFromRoute = Number(routeParams.get('bookId'));
        
        this.booksService.getBook(bookIdFromRoute).pipe(
            tap(book => this.book = book),
            switchMap(_ => this.booksService.getBookInstances(bookIdFromRoute)),
            tap(instances => this.bookInstanses = instances)
        ).subscribe();
    }
}
