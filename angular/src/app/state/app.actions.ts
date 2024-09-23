import {
	createAction,
	createActionGroup,
	emptyProps,
	props,
} from "@ngrx/store";
import { UserProfile } from "../identity/models";
import { Categoria, Item } from "@root/components/models";

export const InitApp = createAction("[App] - Init App");
export const MainToolbarActions = createActionGroup({
	source: "Main Toolbar",
	events: {
		MenuButtonClicked: emptyProps,
		ShoppingCartButtonClicked: emptyProps,
	},
});

export const categoriesObtained = createAction(
	"[App] - Categorias obtenidas",
	props<{ categorias: Categoria[] }>()
);

export const searchButtonClicked = createAction(
	"[Main Toolbar] - Search button clicked"
);
export const profileButtonClicked = createAction(
	"[Main Toolbar] - Profile button clicked"
);
export const backdropClicked = createAction("[Drawer] - Backdrop clicked");

export const SecondaryToolbarActions = createActionGroup({
	source: "Categories Toolbar",
	events: {
		CategoryButtonClicked: props<{ categoriaId: string }>(),
	},
});

export const pantsButtonClicked = createAction(
	"[Secondary toolbar] - Pants button clicked"
);
export const tShirtsButtonClicked = createAction(
	"[Secondary toolbar] - T-Shirts button clicked"
);

export const userProfileObtained = createAction(
	"[Identity] - User profile obtained",
	props<{ userProfile: UserProfile }>()
);

export const itemCargado = createAction(
	"[App] - Item cargado",
	props<{ item: Item }>()
);
