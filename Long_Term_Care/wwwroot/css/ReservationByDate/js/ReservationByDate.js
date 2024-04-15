    document.addEventListener("DOMContentLoaded", function () {
                const calendarEl = document.getElementById("calendar")
    const calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: "dayGridMonth",
    headerToolbar: {
        left: "prev,next today",
    center: "title",
    right: "dayGridMonth,timeGridWeek,timeGridDay",
                    },
    buttonText: {
        today: "Today",
    month: "Month",
    week: "Week",
    day: "Day",
                    },
                })
    calendar.render()
            })

