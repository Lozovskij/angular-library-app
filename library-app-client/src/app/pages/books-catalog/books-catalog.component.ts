import { Component } from '@angular/core';
import { Book } from '../../shared/models/books';
import { BooksService } from '../../shared/services/books.service';

@Component({
    selector: 'app-books-catalog',
    templateUrl: './books-catalog.component.html',
    styleUrls: ['./books-catalog.component.scss']
})
export class BooksCatalogComponent {

    public displayedColumns: string[] = ['title', 'authors'];
    public dataSource: Book[] = [];

    constructor(
        private booksService: BooksService,
    ) {
        booksService.getBooks().subscribe(books => this.dataSource = books);
    }
}
