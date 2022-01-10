import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
@Injectable()
export class LoaderService {
    isLoading = new Subject<boolean>();
    show() {
        console.log("hej");
        this.isLoading.next(true);
    }
    hide() {
        console.log("gej");

        this.isLoading.next(false);
    }
}