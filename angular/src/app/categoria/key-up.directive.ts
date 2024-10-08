import { Directive, ElementRef, HostListener, Input } from "@angular/core";
import { Store } from "@ngrx/store";
import { CategoriesActions } from "./state/categorias.actions";

@Directive({ selector: "[key-up]" })
export class KeyUpDirective {
	@Input()
	public value: string;
	constructor(
		private store: Store,
		private element: ElementRef
	) {}
	@HostListener("keyup")
	public onKeyUp() {
		this.store.dispatch(
			CategoriesActions.searchInputKeyUp({ value: this.value })
		);
	}
}
