import { createAction, createActionGroup, props } from "@ngrx/store";
import { UserProfile } from "../identity/models";
import { Categoria } from "@root/components/models";

export const InitApp = createAction(
    '[App] - Init App'
);

export const categoriesObtained = createAction(
    '[App] - Categorias obtenidas',
    props<{categorias: Categoria[]}>()
)

export const menuButtonClicked = createAction(
    '[Main Toolbar] - Menu button clicked'
);
export const searchButtonClicked = createAction(
    '[Main Toolbar] - Search button clicked'
);
export const shoppingCartButtonClicked = createAction(
    '[Main Toolbar] - Shopping cart button clicked'
);
export const backdropClicked = createAction(
    '[Drawer] - Backdrop clicked'
)
export const profileButtonClicked = createAction(
    '[Main Toolbar] - Profile button clicked'
);

export const SecondaryToolbarActions = createActionGroup({
    source: 'Secondary Toolbar',
    events: {
        ButtonClicked: props<{categoriaId: string}>(),
        CategoriaCargada: props<{categoria: Categoria}>()
    }
})

export const pantsButtonClicked = createAction(
    '[Secondary toolbar] - Pants button clicked'
);
export const tShirtsButtonClicked = createAction(
    '[Secondary toolbar] - T-Shirts button clicked'
);

export const userProfileObtained = createAction(
    '[Identity] - User profile obtained',
    props<{userProfile: UserProfile}>()
)