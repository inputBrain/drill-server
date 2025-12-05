# Drill Management System - Frontend Development Guide

## Technology Stack
- **Framework**: Next.js (App Router)
- **Language**: TypeScript
- **Styling**: Tailwind CSS + shadcn/ui components (crypto exchange style)
- **State Management**: React Query для кешування та оптимізації запитів
- **Toast Notifications**: ToastProvider.tsx (вже існує)
- **Icons**: lucide-react
- **Date/Time**: date-fns для форматування

## Server API Documentation

### Base URL
`http://localhost:5000`

### Authentication
Всі ендпоінти наразі `[AllowAnonymous]` - не потребують автентифікації

---

## API Endpoints & DTOs

### 1. USER Endpoints

#### Create User
- **Method**: `POST`
- **URL**: `/api/User`
- **Request Body** (CreateUser):
```typescript
{
  email?: string | null,      // Optional
  firstName: string,          // Required
  lastName: string            // Required
}
```
- **Response** (UserDto):
```typescript
{
  id: number,
  email: string | null,
  firstName: string,
  lastName: string,
  createdAt: string  // ISO 8601 format: "2025-12-04T19:30:00Z"
}
```

#### List All Users
- **Method**: `GET`
- **URL**: `/api/User/list`
- **Response**: `UserDto[]`

#### Update User
- **Method**: `PATCH`
- **URL**: `/api/User`
- **Request Body** (UpdateUser):
```typescript
{
  userId: number,             // Required
  email?: string | null,      // Optional
  firstName: string,          // Required
  lastName: string            // Required
}
```
- **Response** (UpdateUserResponse):
```typescript
{
  user: UserDto
}
```

#### Delete User
- **Method**: `DELETE`
- **URL**: `/api/User`
- **Request Body** (DeleteUser):
```typescript
{
  userId: number              // Required
}
```
- **Response**: `200 OK`

---

### 2. DRILL Endpoints

#### Create Drill
- **Method**: `POST`
- **URL**: `/api/Drill`
- **Request Body** (CreateDrill):
```typescript
{
  title: string,              // Required
  pricePerMinute: number      // Required (float)
}
```
- **Response** (DrillDto):
```typescript
{
  id: number,
  title: string,
  pricePerMinute: number,
  createdAt: string,          // ISO 8601
  users: UserDrillDto[]       // Масив користувачів які працюють з цим drill
}
```

#### List All Drills
- **Method**: `GET`
- **URL**: `/api/Drill/list`
- **Response**: `DrillDto[]` (кожен drill містить масив users з їх сесіями)

#### Update Drill
- **Method**: `PATCH`
- **URL**: `/api/Drill`
- **Request Body** (UpdateDrill):
```typescript
{
  drillId: number,            // Required
  title: string,              // Required
  pricePerMinute: number      // Required
}
```
- **Response** (UpdateDrillResponse):
```typescript
{
  drill: DrillDto
}
```

#### Delete Drill
- **Method**: `DELETE`
- **URL**: `/api/Drill`
- **Request Body** (DeleteDrill):
```typescript
{
  drillId: number             // Required
}
```
- **Response**: `200 OK`

#### Start Drill
- **Method**: `POST`
- **URL**: `/api/Drill/start`
- **Request Body** (StartDrill):
```typescript
{
  drillId: number,            // Required
  userIds: number[]           // Required - масив ID користувачів
}
```
- **Response** (StartDrillResponse):
```typescript
{
  drill: DrillDto             // Drill з оновленим масивом users
}
```
- **Логіка**: Для кожного userId створюється новий UserDrill запис з StartedAt = поточний час

#### Stop Drill
- **Method**: `POST`
- **URL**: `/api/Drill/stop`
- **Request Body** (StopDrill):
```typescript
{
  drillId: number,            // Required
  userIds: number[]           // Required - масив ID користувачів для зупинки
}
```
- **Response** (StopDrillResponse):
```typescript
{
  drill: DrillDto
}
```
- **Логіка**: Для кожного userId знаходить активну сесію (де StoppedAt === null) і встановлює StoppedAt = поточний час

---

### 3. USERDRILL Endpoints

#### List All UserDrills
- **Method**: `GET`
- **URL**: `/api/UserDrill/list`
- **Response**: `UserDrillDto[]`

#### Get Active UserDrills
- **Method**: `GET`
- **URL**: `/api/UserDrill/active`
- **Response**: `UserDrillDto[]` (тільки ті де StoppedAt === null)

#### Get Completed UserDrills
- **Method**: `GET`
- **URL**: `/api/UserDrill/completed`
- **Response**: `UserDrillDto[]` (тільки ті де StoppedAt !== null)

#### Delete UserDrill
- **Method**: `DELETE`
- **URL**: `/api/UserDrill`
- **Request Body** (DeleteUserDrill):
```typescript
{
  userId: number,             // Required
  drillId: number             // Required
}
```
- **Response**: `200 OK`

---

## Data Models

### UserDrillDto
```typescript
{
  id: number,
  userId: number,
  drillId: number,
  startedAt: string,          // ISO 8601: "2025-12-04T19:30:00Z"
  stoppedAt: string | null,   // null якщо сесія активна
  user: UserDto,              // Вкладений об'єкт користувача
  drill: DrillDto             // Вкладений об'єкт drill (без users щоб уникнути циклічності)
}
```

---

## Date/Time Handling

### С# сервер
- Використовує `DateTimeOffset` (UTC)
- Зберігає в БД як timestamp with time zone
- Відправляє на клієнт в ISO 8601 форматі: `"2025-12-04T19:30:00Z"`

### Frontend (JavaScript/TypeScript)
```typescript
// Отримали з сервера
const startedAt = "2025-12-04T19:30:00Z";

// Конвертуємо в Date
const startDate = new Date(startedAt);

// Розрахунок тривалості для активної сесії
const now = new Date();
const duration = now.getTime() - startDate.getTime(); // в мілісекундах

// Конвертація в години:хвилини:секунди
const totalSeconds = Math.floor(duration / 1000);
const hours = Math.floor(totalSeconds / 3600);
const minutes = Math.floor((totalSeconds % 3600) / 60);
const seconds = totalSeconds % 60;

// Формат: "01:23:45" або "23:45" або "45s"
```

---

## UI/UX Structure

### Layout
```
┌─────────────────────────────────────┐
│           HEADER                     │
│  [Logo] [Home] [Reports] [Users]    │
└─────────────────────────────────────┘
│                                      │
│         MAIN CONTENT                 │
│                                      │
│                                      │
│                                      │
└─────────────────────────────────────┘
│           FOOTER                     │
│  © 2025 Drill Management System     │
└─────────────────────────────────────┘
```

---

## Page 1: Home (Drill Cards)

### Responsive Grid Layout
- **4K/Large screens**: 6 cards per row
- **Full HD (1920px)**: 5 cards per row
- **Laptop (1440px)**: 4 cards per row
- **Tablet (1024px)**: 3 cards per row
- **Mobile (768px)**: 2 cards per row
- **Small mobile (640px)**: 1 card per row

### Drill Card Structure
```
┌────────────────────────────────┐
│  Title: "Дриль #1"      [Edit]│
│  Price: 50 грн/хв      [Delete]│
├────────────────────────────────┤
│  Active Users:                 │
│  ┌──────────────────────────┐ │
│  │ ● John Doe   [▶] 01:23:45│ │ <- активна сесія
│  │   [Stop icon]            │ │
│  ├──────────────────────────┤ │
│  │   Jane Smith  ⏸ 00:45:12│ │ <- зупинена сесія
│  └──────────────────────────┘ │
├────────────────────────────────┤
│  Add Users:                    │
│  [Multi-select dropdown]       │
│  ↓ Select users...             │
├────────────────────────────────┤
│  [START]           [STOP ALL] │
└────────────────────────────────┘
```

### Card Features

#### 1. Header Section
- **Title** - назва drill (клік для редагування inline або модалка)
- **Price/min** - ціна за хвилину (клік для редагування)
- **Edit Icon** - відкриває модалку для редагування
- **Delete Icon** - підтвердження + DELETE `/api/Drill` з drillId

#### 2. Active Users Section
Відображає всіх users які мають UserDrill записи для цього drill:

**Для активної сесії (stoppedAt === null):**
```
● [green pulsing dot] FirstName LastName  [active badge]  01:23:45
                                                          [Stop icon]
```
- Таймер оновлюється кожну секунду
- [Stop icon] - викликає POST `/drill/stop` з `userIds: [userId]`

**Для зупиненої сесії:**
```
⏸ FirstName LastName  [completed badge]  Total: 01:23:45
```

#### 3. User Selector
- Multi-select dropdown
- Показує ТІЛЬКИ тих users які ще НЕ додані до цього drill
- Fetch: GET `/user/list`, filter out users вже в drill.users
- Можна обрати декілька users одночасно

#### 4. Action Buttons

**[START] Button**
- Активна тільки якщо є обрані users в селекторі
- Викликає: POST `/drill/start`
```typescript
{
  drillId: drill.id,
  userIds: [selectedUserId1, selectedUserId2, ...]
}
```
- Після успіху: очистити селектор, оновити список users в карточці

**[STOP ALL] Button**
- Активна тільки якщо є активні сесії (users з stoppedAt === null)
- Викликає: POST `/drill/stop`
```typescript
{
  drillId: drill.id,
  userIds: [всі userId з активними сесіями]
}
```

### Create Drill Card (Пуста карточка в кінці)
```
┌────────────────────────────────┐
│                                │
│         [+ Icon]               │
│                                │
│     Додати новий Drill         │
│                                │
└────────────────────────────────┘
```
- Клік відкриває модалку для створення

### Create/Edit Drill Modal
```
┌──────────────────────────────────────┐
│  Створити новий Drill          [X]   │
├──────────────────────────────────────┤
│                                      │
│  Назва:                              │
│  [___________________________]       │
│                                      │
│  Ціна за хвилину (грн):              │
│  [___________________________]       │
│                                      │
│  Додати користувачів (опціонально): │
│  [Multi-select ▼]                   │
│                                      │
│          [Скасувати]  [Створити]    │
└──────────────────────────────────────┘
```

**Логіка створення:**
1. POST `/drill` з title та pricePerMinute
2. Отримати створений drill з response
3. Якщо були обрані users → POST `/drill/start` з drillId та userIds
4. Закрити модалку, додати нову карточку в grid

---

## Page 2: Reports

### Layout
```
┌────────────────────────────────────┐
│  Звіти                             │
│  Детальна інформація про всі       │
│  drill сесії користувачів          │
└────────────────────────────────────┘

┌────────────────────────────────────┐
│  Підсумкова статистика             │
│  [Всі записи] [Активні] [Зупинені]│
├────┬──────────┬──────────┬─────────┤
│Drill│Сесії    │Час       │Вартість │
├────┼──────────┼──────────┼─────────┤
│... │...       │...       │...      │
├────┼──────────┼──────────┼─────────┤
│         Загалом: 123  45:30:15  5670грн│
└────────────────────────────────────┘

┌────────────────────────────────────┐
│  Всі сесії                         │
├──────┬──────┬──────┬──────┬────┬───┤
│User  │Drill │Початок│Stop │Час│Вар│
├──────┼──────┼──────┼──────┼────┼───┤
│...   │...   │...   │...   │... │ 🗑│
├──────┴──────┴──────┴──────┴────┴───┤
│  Загальний підсумок: 123:45:30 12340грн│
└────────────────────────────────────┘
```

### Table 1: Підсумкова статистика

**Filters:**
- **Всі записи**: GET `/userdrill/list`
- **Тільки Активні**: GET `/userdrill/active`
- **Зупинені**: GET `/userdrill/completed`

**Columns:**
1. **Назва Drill** (drill.title)
2. **Кількість сесій** - підрахунок скільки разів цей drill використовувався
3. **Загальний час** - сума всіх тривалостей сесій цього drill
   - Формат: `45:30:15` (години:хвилини:секунди)
   - Для активних сесій: `now - startedAt`
   - Для зупинених: `stoppedAt - startedAt`
4. **Загальна вартість** (грн)
   - `pricePerMinute * (загальний час в хвилинах)`
   - Оновлюється в реальному часі для активних сесій

**Footer Row:**
- Сума всіх колонок
- Оновлюється в реальному часі

**Calculation Logic:**
```typescript
// Для кожного drill
const sessions = userDrills.filter(ud => ud.drillId === drill.id);

const totalDuration = sessions.reduce((acc, session) => {
  const start = new Date(session.startedAt);
  const end = session.stoppedAt ? new Date(session.stoppedAt) : new Date();
  return acc + (end.getTime() - start.getTime());
}, 0);

const totalMinutes = totalDuration / 1000 / 60;
const totalCost = totalMinutes * drill.pricePerMinute;
```

### Table 2: Всі сесії

**Data Source:**
- GET `/userdrill/list` (або active/completed з фільтрів)

**Columns:**
1. **Користувач** - `${user.firstName} ${user.lastName}`
2. **Drill** - `drill.title`
3. **Дата початку** - `startedAt` formatted як "04.12.2025 19:30"
4. **Зупинка**
   - Якщо `stoppedAt === null`: `[active]` badge з зеленою точкою
   - Інакше: дата в форматі "04.12.2025 20:15"
5. **Тривалість** (real-time)
   - Якщо активна: оновлюється кожну секунду
   - Формати:
     - < 1 min: "45s"
     - < 1 hour: "23:45"
     - >= 1 hour: "01:23:45"
6. **Ціна/хв** - `drill.pricePerMinute` грн
7. **Вартість** (real-time)
   - `(тривалість в хвилинах) * pricePerMinute`
   - Оновлюється для активних сесій
8. **Дії** - 🗑️ Delete icon
   - DELETE `/userdrill` з `userId` та `drillId`

**Footer Row: Загальний підсумок**
- Загальний час всіх сесій (оновлюється в реальному часі)
- Загальна вартість (оновлюється в реальному часі)

---

## Real-time Updates Strategy

### Проблема
Активні сесії потребують оновлення таймерів та вартості кожну секунду

### Рішення

#### 1. Client-side таймери
```typescript
useEffect(() => {
  const interval = setInterval(() => {
    // Оновити тільки компоненти з активними сесіями
    setCurrentTime(new Date());
  }, 1000);

  return () => clearInterval(interval);
}, []);
```

#### 2. React Query для даних
```typescript
// Автоматичне оновлення кожні 30 секунд
const { data: drills } = useQuery({
  queryKey: ['drills'],
  queryFn: fetchDrills,
  refetchInterval: 30000,
  staleTime: 10000
});
```

#### 3. Оптимізація ре-рендерів
- Використовувати `React.memo` для карточок
- Оновлювати тільки активні таймери
- Дебаунс для інпутів

---

## API Request Optimization

### React Query Configuration
```typescript
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 5 * 60 * 1000,  // 5 хвилин
      cacheTime: 10 * 60 * 1000,  // 10 хвилин
      refetchOnWindowFocus: false,
      retry: 1
    }
  }
});
```

### Запобігання дублювання запитів
1. **Використовувати React Query** - автоматично дедуплікує однакові запити
2. **Invalidation стратегія**:
```typescript
// Після створення drill
queryClient.invalidateQueries(['drills']);

// Після start/stop
queryClient.invalidateQueries(['drills', drillId]);
queryClient.invalidateQueries(['userdrills']);
```

3. **Оптимістичні оновлення** для швидкого UX:
```typescript
const mutation = useMutation({
  mutationFn: startDrill,
  onMutate: async (newData) => {
    // Скасувати поточні запити
    await queryClient.cancelQueries(['drills']);

    // Оптимістично оновити кеш
    queryClient.setQueryData(['drills'], (old) => {
      // ... оновити локально
    });
  },
  onError: (err, variables, context) => {
    // Відкотити при помилці
    queryClient.setQueryData(['drills'], context.previousDrills);
  },
  onSettled: () => {
    // Перезапитати для синхронізації
    queryClient.invalidateQueries(['drills']);
  }
});
```

---

## Styling Guidelines

### Design System (Crypto Exchange Style)

**Color Palette:**
- Background: Dark mode preferred (#0a0e27, #151a30)
- Cards: Slightly lighter (#1a1f3a)
- Accent: Blue/Cyan (#00d4ff, #0066ff)
- Success: Green (#00ff88)
- Danger: Red (#ff3366)
- Text: White (#ffffff) / Gray (#a0a0a0)

**Typography:**
- Font: Inter, SF Pro, or similar modern sans-serif
- Headings: 24px, 20px, 18px (font-weight: 600)
- Body: 14px, 16px (font-weight: 400)

**Components:**
- Rounded corners: 8px-12px
- Shadows: Subtle, colored (rgba(0, 212, 255, 0.1))
- Hover effects: Scale 1.02, glow effects
- Transitions: 200ms ease-in-out

**Buttons:**
- Primary: Gradient blue (#0066ff → #00d4ff)
- Success: Green (#00ff88)
- Danger: Red (#ff3366)
- Disabled: Gray (#2a2f45)

**Badges:**
- Active: Green bg, pulsing dot animation
- Completed: Gray bg

### shadcn/ui Components to Use
- Card, CardHeader, CardContent
- Button
- Dialog (Modal)
- Select (Multi-select with Combobox)
- Table
- Badge
- Toast
- DropdownMenu

### Mobile Considerations
- Touch targets min 44px
- Swipe gestures for cards
- Bottom sheets for modals on mobile
- Sticky header on scroll
- Hamburger menu for navigation

---

## Navigation

### Header Navigation
```typescript
<nav>
  <Link href="/">
    <Logo />
  </Link>
  <Link href="/">Home</Link>
  <Link href="/reports">Reports</Link>
  <Link href="/users">Users</Link> {/* Future page */}
</nav>
```

**Important:** Використовувати Next.js `<Link>` для client-side navigation без перезавантаження сторінки

---

## Error Handling

### Toast Notifications
```typescript
// Success
toast.success('Drill успішно створено!');

// Error
toast.error('Помилка при створенні drill');

// Info
toast.info('Сесію зупинено');
```

### API Error Handling
```typescript
try {
  const response = await fetch('/api/Drill', {
    method: 'POST',
    body: JSON.stringify(data)
  });

  if (!response.ok) {
    throw new Error('Помилка сервера');
  }

  const result = await response.json();
  return result;
} catch (error) {
  toast.error(error.message);
  throw error;
}
```

---

## Responsive Breakpoints

```typescript
// tailwind.config.js
module.exports = {
  theme: {
    screens: {
      'sm': '640px',
      'md': '768px',
      'lg': '1024px',
      'xl': '1280px',
      '2xl': '1536px',
      '3xl': '1920px',
      '4xl': '2560px'
    }
  }
}
```

**Grid columns:**
```typescript
<div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:grid-cols-6 gap-4">
  {/* Drill cards */}
</div>
```

---

## Key Features Checklist

### Home Page
- ✅ Grid of drill cards (responsive)
- ✅ Each card shows drill info (title, price)
- ✅ Active users list with real-time timers
- ✅ Multi-select dropdown for adding users
- ✅ START button (only active when users selected)
- ✅ STOP ALL button (only active when has active sessions)
- ✅ Individual STOP icon per user
- ✅ Edit drill (inline or modal)
- ✅ Delete drill (with confirmation)
- ✅ Create drill card (empty card with +)
- ✅ Create drill modal
- ✅ Active badge with pulsing green dot

### Reports Page
- ✅ Summary statistics table
- ✅ Filters (All/Active/Completed)
- ✅ Drill name, session count, total time, total cost
- ✅ Real-time updates for active sessions
- ✅ Footer row with totals
- ✅ All sessions table
- ✅ User, Drill, Start date, Stop date/Active badge
- ✅ Duration (real-time formatting)
- ✅ Price/min, Cost (real-time)
- ✅ Delete session icon
- ✅ Footer with grand totals

### General
- ✅ Header with navigation (Home, Reports)
- ✅ Footer
- ✅ Toast notifications
- ✅ Responsive design (6-5-4-3-2-1 columns)
- ✅ Mobile optimized
- ✅ Dark mode crypto exchange style
- ✅ No redundant API calls
- ✅ React Query caching
- ✅ Optimistic updates
- ✅ Error handling

---

## Performance Optimization

### Code Splitting
```typescript
// Lazy load modals
const CreateDrillModal = dynamic(() => import('./CreateDrillModal'), {
  loading: () => <Skeleton />
});
```

### Memoization
```typescript
const DrillCard = React.memo(({ drill }) => {
  // ...
});

const calculateCost = useMemo(() => {
  return duration * pricePerMinute;
}, [duration, pricePerMinute]);
```

### Virtual Scrolling (для великих таблиць)
```typescript
import { useVirtualizer } from '@tanstack/react-virtual';
```

---

## Development Workflow

### 1. Setup
```bash
npx create-next-app@latest drill-frontend
cd drill-frontend
npm install @tanstack/react-query date-fns lucide-react
npx shadcn-ui@latest init
```

### 2. Project Structure
```
app/
  layout.tsx
  page.tsx           # Home (Drill cards)
  reports/
    page.tsx         # Reports page
  api/               # API routes (proxy to backend)
components/
  drill-card.tsx
  create-drill-modal.tsx
  user-selector.tsx
  active-session.tsx
  header.tsx
  footer.tsx
lib/
  api.ts             # API client functions
  utils.ts           # Helper functions (duration, cost calc)
hooks/
  use-drills.ts      # React Query hooks
  use-timer.ts       # Real-time timer hook
types/
  index.ts           # TypeScript interfaces
```

### 3. API Client Example
```typescript
// lib/api.ts
export const api = {
  drills: {
    list: () => fetch('/api/Drill/list').then(r => r.json()),
    create: (data: CreateDrill) =>
      fetch('/api/Drill', {
        method: 'POST',
        body: JSON.stringify(data)
      }).then(r => r.json()),
    update: (data: UpdateDrill) =>
      fetch('/api/Drill', {
        method: 'PATCH',
        body: JSON.stringify(data)
      }).then(r => r.json()),
    delete: (drillId: number) =>
      fetch('/api/Drill', {
        method: 'DELETE',
        body: JSON.stringify({ drillId })
      }),
    start: (data: StartDrill) =>
      fetch('/api/Drill/start', {
        method: 'POST',
        body: JSON.stringify(data)
      }).then(r => r.json()),
    stop: (data: StopDrill) =>
      fetch('/api/Drill/stop', {
        method: 'POST',
        body: JSON.stringify(data)
      }).then(r => r.json())
  },
  users: {
    list: () => fetch('/api/User/list').then(r => r.json()),
    create: (data: CreateUser) =>
      fetch('/api/User', {
        method: 'POST',
        body: JSON.stringify(data)
      }).then(r => r.json()),
    update: (data: UpdateUser) =>
      fetch('/api/User', {
        method: 'PATCH',
        body: JSON.stringify(data)
      }).then(r => r.json()),
    delete: (userId: number) =>
      fetch('/api/User', {
        method: 'DELETE',
        body: JSON.stringify({ userId })
      })
  },
  userDrills: {
    list: () => fetch('/api/UserDrill/list').then(r => r.json()),
    active: () => fetch('/api/UserDrill/active').then(r => r.json()),
    completed: () => fetch('/api/UserDrill/completed').then(r => r.json()),
    delete: (userId: number, drillId: number) =>
      fetch('/api/UserDrill', {
        method: 'DELETE',
        body: JSON.stringify({ userId, drillId })
      })
  }
};
```

---

## Summary

Цей проект це **професійний drill tracking system** з:
- ✅ Сучасним Next.js frontend
- ✅ Real-time таймерами та калькуляціями
- ✅ Crypto exchange стилізацією
- ✅ Повною адаптивністю
- ✅ Оптимізованими API запитами
- ✅ Чудовим UX на всіх девайсах

**Головні принципи:**
1. Performance First - React Query + оптимізація
2. UX First - real-time updates, інтуїтивний інтерфейс
3. Mobile First - responsive design
4. Type Safety - TypeScript всюди
5. Best Practices - код як у топових компаніях
