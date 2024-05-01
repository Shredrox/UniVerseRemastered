import CalendarIcon from '../assets/icons/icon-calendar.svg'
import TaskIcon from '../assets/icons/icon-task.svg'
import CourseIcon from '../assets/icons/icon-course.svg'
import { Calendar } from '../components/Calendar'
import { useState } from 'react'
import { addMonths, isSameMonth } from 'date-fns'

const Courses = () => {
  const [date, setDate] = useState<Date | undefined>(new Date())
  const today = new Date();
  const nextMonth = addMonths(new Date(), 0);
  const [month, setMonth] = useState<Date>(nextMonth);

  const bookedDays = [new Date(2024, 5, 8), new Date(2024, 5, 9)];
  const bookedStyle = { border: '2px solid currentColor' };

  return (
    <div className="w-full h-[800px] flex justify-start flex-col gap-6 items-center overflow-hidden">
      <div className="flex w-[80%] gap-4">
        <div className="bg-[#01152a] w-fit h-[450px] flex flex-col items-center p-6 rounded-2xl">
          <span className='flex items-center gap-2 w-full text-2xl'>
            <img className='w-9 h-9' src={CalendarIcon}/> 
            Calendar
          </span>
          <Calendar
            month={month}
            onMonthChange={setMonth}
            mode="single"
            weekStartsOn={1}
            fixedWeeks
            modifiers={{ booked: bookedDays }}
            modifiersStyles={{ booked: bookedStyle }}
            selected={date}
            onSelect={setDate}
            className="rounded-md border"
          />
          <button 
            className='confirm-button'
            disabled={isSameMonth(today, month)}
            onClick={() => setMonth(today)}
          >
            Go to Today
          </button>
        </div>
        <div className="bg-[#01152a] w-full h-[450px] flex flex-col items-center p-6 rounded-2xl">
          <span className='flex items-center gap-2 w-full text-2xl'>
            <img className='w-9 h-9' src={TaskIcon}/> 
            Assignments
          </span>
        </div>
      </div>
      <div className='bg-[#01152a] w-[80%] h-[456px] flex flex-col items-center p-6 rounded-2xl'>
        <span className='flex items-center gap-2 w-full text-2xl'>
          <img className='w-9 h-9' src={CourseIcon}/> 
          Materials
        </span>
      </div>
    </div>
  )
}

export default Courses
