import { Book } from "./books";

export interface Patron {
    id: number;
    fullName: string;
    cardNumber: string;
    phoneNumber: string | null;
    email: string | null;
    //TODO how to add picture

    holds: Book[];
    checkedOutBooks: Book[];
}

export const user: Patron = {
    id: 1,
    fullName: 'Ivan Ivanov',
    cardNumber: '430H',
    phoneNumber: '+375 29 233 33 23',
    email: 'ivan.ivanov@test.mail',
    holds: [],
    checkedOutBooks: [],
}