import { createAction, props } from "@ngrx/store";

export const ShoppingCartButtonClicked = createAction(
    '[Item Card] - Shopping cart button clicked',
    props<{itemId: string}>()
);

export const DetailsButtonClicked = createAction(
    '[Item Card] - Details button clicked',
    props<{itemId: string}>()
);