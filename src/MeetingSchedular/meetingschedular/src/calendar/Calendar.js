import React, {useEffect, useRef, useState} from 'react';
import {DayPilot, DayPilotCalendar, DayPilotNavigator} from "@daypilot/daypilot-lite-react";
import "./Calendar.css";
import {ResourceGroups} from "./ResourceGroups";
import axios from 'axios';
import { v4 as uuidv4 } from 'uuid';

const Calendar = () => {
  const calendarRef = useRef();

  const [groups, setGroups] = useState([]);
  const [events, setEvents] = useState([]);
  const [columns, setColumns] = useState([]);
  const [startDate, setStartDate] = useState(new DayPilot.Date("2024-04-09"));
  const [selected, setSelected] = useState();
  
  
    const [users, setUsers] = useState([]);

    useEffect(() => {
      const fetchPeople = async () => {
        try{
          const response = await axios.get('https://localhost:44372/api/Users');
          setUsers(response.data);

        } catch (error) {
        console.error('Error fetching users:', error);
    }
  };
    fetchPeople();
},[]); 
    

    useEffect(() => {
        const fetchEvents = async () => {
            try {
                const response = await axios.get('https://localhost:44372/api/Meetings');
                setEvents(response.data);
            } catch (error) {
                console.error('Error fetching events:', error);
            }
        };

        fetchEvents();
    }, [startDate, columns]);
  
  const [config, setConfig] = useState({
    viewType: "Resources",
    timeRangeSelectedHandling: "Enabled",
    eventDeleteHandling: "Update",
    headerHeight: 40,
  });
  
  useEffect(() => {

    const data = [            
    {
      name: "People",
      id: "people",
      resources: [users.map(user => ({
          name: user.firstName, 
          id: user.id
      }))]
  },
  { name: "Locations", id: "locations", resources: [
    {name: "Room 1", id: "R1"},
    {name: "Room 2", id: "R2"},
    {name: "Room 3", id: "R3"},
    {name: "Room 4", id: "R4"},
    {name: "Room 5", id: "R5"},
    {name: "Room 6", id: "R6"},
    {name: "Room 7", id: "R7"},
  ]
}
  ]
    setGroups(data);
    setSelected(data[0]);

  }, []);

  useEffect(() => {
    setColumns(selected?.resources || []);
  }, [selected, groups]);

  const next = () => {
    setStartDate(startDate.addDays(1));
  };

  const previous = () => {
    setStartDate(startDate.addDays(-1));
  };


  const addEvent = async (id, name, start, end, userid) => {
    try {
        await axios.post('https://localhost:44372/api/Meetings', {
            id,
            name,
            start: start,
            end: end,
            userid
        });        
        console.log('Event added successfully');
    } catch (error) {
        console.error('Error adding event:', error);
    }
  };

  const updateEvent = async (id, name, start,end) => {
    try {
        await axios.put('https://localhost:44372/api/Meetings/${id}',  {
            id: id,
            meeting:{name: name,
            start: start,
            end: end,attendees:[]
          }
            
        });        
        console.log('Event added successfully');
    } catch (error) {
        console.error('Error adding event:', error);
    }
  };

  const onTimeRangeSelected = async args => {
    const modal = await DayPilot.Modal.prompt("Create a new event:", "Event 1");
    calendarRef.current.control.clearSelection();
    if (modal.canceled) { return; }
    addEvent(uuidv4(),modal.result, args.start, args.end, 'd8ac77a0-71ac-4835-954d-e6004aa8bd6d');
    setEvents([...events]);
  };

  const onEventClick = async args => {
    const modal = await DayPilot.Modal.prompt("Update event text:", args.e.text());
    if (!modal.result) { return; }
    args.e.data.text = modal.result;
    updateEvent(modal.id, modal.result, args.start, args.end);
    setEvents([...events]);
  };

  const onBeforeHeaderRender = args => {
    args.header.areas = [
      {
        right: "5",
        top: 5,
        width: 30,
        height: 30,
        symbol: "/daypilot.svg#edit",
        padding: 6,
        cssClass: "icon",
        toolTip: "Edit...",
        onClick: async args => {
          const column = args.source;
          const modal = await DayPilot.Modal.prompt("Resource name:", column.name);
          if (!modal.result) { return; }
          column.data.name = modal.result;
          setColumns([...columns]);
        }
      }
    ];
  };

  return (
    <div className={"wrap"}>
      <div className={"left"}>
        <DayPilotNavigator
          selectMode={"Day"}
          showMonths={3}
          skipMonths={3}
          selectionDay={startDate}
          startDate={startDate}
          onTimeRangeSelected={ args => setStartDate(args.day) }
        />
      </div>
      <div className={"calendar"}>

        <div className={"toolbar"}>
          <ResourceGroups onChange={args => setSelected(args.selected)} items={groups}></ResourceGroups>
          <span>Day:</span>
          <button onClick={ev => previous()}>Previous</button>
          <button onClick={ev => next()}>Next</button>
        </div>

        <DayPilotCalendar
          {...config}
          onTimeRangeSelected={onTimeRangeSelected}
          onEventClick={onEventClick}
          onBeforeHeaderRender={onBeforeHeaderRender}
          startDate={startDate}
          events={events.map(event => ({
            id: event.id,
            text: event.name,
            start: event.start, 
            end: event.end}))}
          columns={columns}
          ref={calendarRef}
        />
      </div>
    </div>
  );
}
export default Calendar;