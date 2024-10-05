import { MdAccountCircle } from 'react-icons/md'
export default function Header() {
    return (
        <header className="bg-gray-800 h-1/6 p-2 flex flex-row justify-start items-center">
        <h1>YoruStore</h1>
        <span className='flex-auto'></span>
        <MdAccountCircle />
      </header>
    )
}