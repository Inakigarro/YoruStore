import { Injectable, OnDestroy } from "@angular/core";
import { WebApiService } from "../web-api.service";
import { Login } from "@root/components/models";
import { filter, Observable, Subject, takeUntil } from "rxjs";
import { Store } from "@ngrx/store";
import { getLoggedIn, getToken } from "./state/auth.selectors";

@Injectable({
	providedIn: "root",
})
export class AuthService implements OnDestroy {
	public destroy$ = new Subject();
	public isLoggedIn$: Observable<boolean>;
	public token$: Observable<string>;

	constructor(private store: Store) {
		this.isLoggedIn$ = this.store.select(getLoggedIn);
		this.token$ = this.store.select(getToken);
	}

	public isAuthenticated() {
		let isAuthenticated: boolean = false;
		this.isLoggedIn$.pipe(takeUntil(this.destroy$)).subscribe((isLoggedIn) => {
			isAuthenticated = isLoggedIn;
		});
		return isAuthenticated;
	}

	public ngOnDestroy(): void {
		this.destroy$.next({});
		this.destroy$.complete();
	}
}
