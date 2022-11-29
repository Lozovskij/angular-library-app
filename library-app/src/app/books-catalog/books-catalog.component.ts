import { Component } from '@angular/core';
import { Book, books } from '../books';

@Component({
  selector: 'app-books-catalog',
  templateUrl: './books-catalog.component.html',
  styleUrls: ['./books-catalog.component.sass']
})
export class BooksCatalogComponent {
  displayedColumns: string[] = ['title', 'author'];
  dataSource: Book[] = books;
}
