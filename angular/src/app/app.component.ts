import { Component, OnInit } from "@angular/core";
import { AppService } from "./app.service";
import { RegisterToolbar } from "@root/components/toolbar/state/toolbar.actions";
import {
	backdropClicked,
	InitApp,
	profileButtonClicked,
	searchButtonClicked,
	userProfileObtained,
	MainToolbarActions,
} from "./state/app.actions";
import { Observable } from "rxjs";
import { Item, Toolbar } from "@root/components/models";
import { UserProfile } from "./identity/models";
import { MAIN_TOOLBAR_ID, SECONDARY_TOOLBAR_ID } from "./app-constants";

@Component({
	selector: "app-root",
	templateUrl: "./app.component.html",
	styleUrl: "./app.component.scss",
})
export class AppComponent implements OnInit {
	public mainToolbar$: Observable<Toolbar | undefined>;
	public secondaryToolbar$: Observable<Toolbar | undefined>;
	public isMenuOpened$: Observable<boolean>;
	public currentUserProfile$: Observable<UserProfile | undefined>;
	public isShoppingCartOpened$: Observable<boolean>;
	public isAdmin: boolean = true;

	constructor(private service: AppService) {
		this.service.dispatch(InitApp());
		this.service.dispatch(
			userProfileObtained({
				userProfile: {
					id: "1",
					loginId: "Zaky",
					nombre: "Iñaki",
					apellido: "Garro",
					email: "email@email.com",
				},
			})
		);
	}

	public ngOnInit(): void {
		this.mainToolbar$ = this.service.getToolbarById(MAIN_TOOLBAR_ID);
		this.secondaryToolbar$ = this.service.getToolbarById(SECONDARY_TOOLBAR_ID);
		this.isMenuOpened$ = this.service.isMenuOpened$;
		this.currentUserProfile$ = this.service.currentUserProfile$;
		this.isShoppingCartOpened$ = this.service.isShoppingCartOpened$;
		this.service.dispatch(
			RegisterToolbar({
				toolbar: {
					id: MAIN_TOOLBAR_ID,
					mainButton: {
						type: "icon",
						icon: "menu",
						label: "",
						action: MainToolbarActions.menuButtonClicked(),
					},
					title: "Yoru Store",
					secondaryButton: [
						{
							type: "basic",
							icon: "search",
							label: "Buscar",
							action: searchButtonClicked(),
						},
						{
							type: "shopping",
							icon: "shopping_cart",
							label: "",
							action: MainToolbarActions.shoppingCartButtonClicked(),
						},
						{
							type: "icon",
							icon: "account_circle",
							label: "",
							action: profileButtonClicked(),
						},
					],
					toolbarConfig: {
						isTitleSeparete: true,
						isSecondaryToolbar: false,
					},
				},
			})
		);
	}

	public onBackdropClicked() {
		this.service.dispatch(backdropClicked());
	}
}
