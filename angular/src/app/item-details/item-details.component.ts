import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Button, Item, Toolbar } from "@components/models";
import {
	ItemDetailsService,
	DETAILS_CARD_TOOLBAR,
} from "./item-details.service";
import { filter, Observable, Subject, takeUntil } from "rxjs";
import { RegisterToolbar } from "@components/toolbar/state/toolbar.actions";
import {
	detailsAddShoppingCartButtonClicked,
	detailsBackButtonClicked,
	detailsBuyButtonClicked,
} from "./state/item-details.actions";

@Component({
	selector: "item-details",
	templateUrl: "item-details.component.html",
	styleUrl: "item-details.component.scss",
})
export class ItemDetailsComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	public item$: Observable<Item | undefined>;
	public cardToolbar$: Observable<Toolbar | undefined>;
	public backButton: Button = {
		type: "icon",
		label: "",
		icon: "arrow_back",
		action: detailsBackButtonClicked(),
	};
	public buyButton: Button;
	public addToCartButon: Button;
	constructor(private service: ItemDetailsService) {}

	public ngOnInit(): void {
		this.item$ = this.service.currentItem$;
		this.item$.subscribe((item) => {
			this.buyButton = {
				type: "raised",
				label: "Comprar ahora",
				icon: "payments",
				action: detailsBuyButtonClicked({ item: item as Item }),
			};
			this.addToCartButon = {
				type: "basic",
				label: "Agregar al carrito",
				icon: "shopping_cart",
				action: detailsAddShoppingCartButtonClicked({
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
