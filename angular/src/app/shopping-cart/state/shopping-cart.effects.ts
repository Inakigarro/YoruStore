import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ShoppingCartService } from "../shopping-cart.service";
import { ShoppingCartButtonClicked } from "@components/card/state/card.actions";
import { map, withLatestFrom } from "rxjs/operators";
import { ItemAddedToShoppingCart } from "./shopping-cart.actions";
import { Item } from "@components/models";

@Injectable()
export class ShoppingCartEffects {
	public upsertItem$ = createEffect(() =>
		this.actions.pipe(
			ofType(ShoppingCartButtonClicked),
			withLatestFrom(this.service.items$),
			map(([action, items]) => {
				let item = items.find((x) => x.id == action.item.id);
				if (item) {
					let updatedItem: Item = {
						id: item.id,
						titulo: item.titulo,
						descripcion: item.descripcion,
						cantidad: item.cantidad ? item.cantidad + 1 : 1,
						precio: item.precio,
					};
					return ItemAddedToShoppingCart({ item: updatedItem });
				}
				return ItemAddedToShoppingCart({
					item: {
						...action.item,
						cantidad: 1,
					},
				});
			})
		)
	);
	constructor(
		private actions: Actions,
		private readonly service: ShoppingCartService
	) {}
}
