import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book, BookInstance, BookInstanceStatus } from '../models/books';

@Injectable({
    providedIn: 'root'
})
export class BooksService {

    constructor(private http: HttpClient) { }

    getBooks(): Observable<Book[]> {
        return this.http.get<Book[]>('/api/books');
    }

    getBook(id: number): Observable<Book> {
        return this.http.get<Book>(`/api/books/${id}`, {
            params: { id: id.toString() }
        });
    }

    getBookInstanceStatus(bookId: number): Observable<BookInstanceStatus | null> {
        return this.http.get<BookInstanceStatus>(`/api/books/${bookId}/instance-status`);
    }

    holdBook(bookId: number): Observable<any> {
        return this.http.post(`/api/books/${bookId}/hold`, {}, {
            params: {
                bookId: bookId.toString()
            }
        });
    }

    cancelBookHold(bookId: number): Observable<any> {
        return this.http.post(`/api/books/${bookId}/cancel-hold`, {}, {
            params: {
                bookId: bookId.toString()
            }
        });
    }
}
