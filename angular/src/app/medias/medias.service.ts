import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import {
	getCurrentCategoryItems,
	getCurrentCategoryName,
} from "../state/app.selectors";

@Injectable({
	providedIn: "root",
})
export class MediasService {
	public title$ = this.store.select(getCurrentCategoryName);
	public data$ = this.store.select(getCurrentCategoryItems);
	constructor(private store: Store) {}
}
