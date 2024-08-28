import { createAction } from "@ngrx/store";

export const menuButtonClicked = createAction(
    '[Main Toolbar] - Menu button clicked'
);
export const searchButtonClicked = createAction(
    '[Main Toolbar] - Search button clicked'
);
export const shoppingCartButtonClicked = createAction(
    '[Main Toolbar] - Shopping cart button clicked'
);
export const profileButtonClicked = createAction(
    '[Main Toolbar] - Profile button clicked'
);

export const pantsButtonClicked = createAction(
    '[Secondary toolbar] - Pants button clicked'
);
export const tShirtsButtonClicked = createAction(
    '[Secondary toolbar] - T-Shirts button clicked'
);