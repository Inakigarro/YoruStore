import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { Item, Toolbar } from "@root/components/models";
import { map, Observable } from "rxjs";
import { getCurrentItem } from "../state/app.selectors";
import { selectAllToolbars } from "@root/components/toolbar/state/toolbar.selectors";

export const DETAILS_CARD_TOOLBAR = "details-toolbar";

@Injectable({
	providedIn: "root",
})
export class ItemDetailsService {
	public currentItem$: Observable<Item | undefined>;
	public cardToolbar$: Observable<Toolbar | undefined>;

	constructor(private store: Store) {
		this.currentItem$ = this.store.select(getCurrentItem);
		this.cardToolbar$ = this.store
			.select(selectAllToolbars)
			.pipe(
				map((toolbars) => toolbars.find((t) => t.id == DETAILS_CARD_TOOLBAR))
			);
	}

	public dispatch(action: Action) {
		this.store.dispatch(action);
	}
}
