import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { getShoppingCartCount } from "@root/app/state/app.selectors";
import { Button } from "@root/components/models";
import { Observable, Subject, takeUntil } from "rxjs";

@Component({
    selector: 'button-feature',
    templateUrl: 'button-feature.component.html'
})
export class ButtonFeatureComponent implements OnInit, OnDestroy {
    private destroy$ = new Subject<void>();
    public shoppingBadgeCount$: Observable<number>;
    public badgeHidden: boolean = false;

    @Input()
    public button: Button;

    constructor(private store: Store){}

    public ngOnInit(): void {
        this.shoppingBadgeCount$ = this.store.select(getShoppingCartCount);
        this.shoppingBadgeCount$.pipe(takeUntil(this.destroy$)).subscribe(count => this.badgeHidden = count <= 0);
    }
    public ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    public dispatch(action: Action) {
        this.store.dispatch(action);
    }
}