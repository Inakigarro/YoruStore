import { Component, OnDestroy, OnInit } from "@angular/core";
import { Button, Item } from "@components/models";
import { ItemDetailsService } from "./item-details.service";
import { Observable, Subject } from "rxjs";
import { ItemDetailsActions } from "./state/item-details.actions";

@Component({
	selector: "item-details",
	templateUrl: "item-details.component.html",
	styleUrl: "item-details.component.scss",
})
export class ItemDetailsComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	public item$: Observable<Item | undefined>;
	public loaded$: Observable<boolean>;
	public backButton: Button = {
		type: "icon",
		label: "",
		icon: "arrow_back",
		action: ItemDetailsActions.backButtonClicked(),
	};
	public buyButton: Button;
	public addToCartButon: Button;
	constructor(private service: ItemDetailsService) {}

	public ngOnInit(): void {
		this.item$ = this.service.currentItem$;
		this.loaded$ = this.service.loaded$;
		this.item$.subscribe((item) => {
			this.buyButton = {
				type: "raised",
				label: "Comprar ahora",
				icon: "payments",
				action: ItemDetailsActions.buyButtonClicked({ item: item as Item }),
			};
			this.addToCartButon = {
				type: "basic",
				label: "Agregar al carrito",
				icon: "shopping_cart",
				action: ItemDetailsActions.addShoppingCartButtonClicked({
					item: item as Item,
				}),
			};
		});
	}

	public ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
	}
}
