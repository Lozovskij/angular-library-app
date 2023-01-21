import { Book } from "./books";

export interface Patron {
    id: number;
    firstName: string;
    lastName: string;
    cardNumber: string;
    
    //TODO how to add picture

    phoneNumber: string | null;
    email: string | null;
    holds: Book[];
    checkedOutBooks: Book[];
}

export const user: Patron = {
    id: 1,
    firstName: "Ivan",
    lastName: "Ivanov",
    cardNumber: '430H',
    phoneNumber: '+375 29 233 33 23',
    email: 'ivan.ivanov@test.mail',
    holds: [],
    checkedOutBooks: [],
}