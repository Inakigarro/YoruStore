import './sidebar.css'

export default function SideBar({buttonLabels}: {buttonLabels: string[]}) {
    const buttons = buttonLabels.map(label => <button key={label}>{label}</button>)
    return (
        <nav className="
        bg-gray-700 h-full sidebar-width flex flex-col justify-evenly">
            {buttons}
        </nav>
    )
}